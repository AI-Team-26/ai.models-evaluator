import { describe, it, expect, vi, beforeEach, afterEach } from 'vitest';
import { render, screen, fireEvent, cleanup, act } from '@testing-library/react';
import SearchBox from './SearchBox';

function deferred() {
  let resolve, reject;
  const promise = new Promise((res, rej) => {
    resolve = res;
    reject = rej;
  });
  return { promise, resolve, reject };
}

describe('SearchBox', () => {
  beforeEach(() => {
    vi.useFakeTimers();
  });

  afterEach(() => {
    cleanup();
    vi.useRealTimers();
  });

  it('debounces rapid keystrokes into a single call', async () => {
    const onSearch = vi.fn().mockResolvedValue([{ id: 1, name: 'Widget' }]);
    render(<SearchBox onSearch={onSearch} />);
    const input = screen.getByPlaceholderText(/search/i);

    fireEvent.change(input, { target: { value: 'w' } });
    fireEvent.change(input, { target: { value: 'wi' } });
    fireEvent.change(input, { target: { value: 'wid' } });

    await act(async () => {
      vi.advanceTimersByTime(300);
    });

    expect(onSearch).toHaveBeenCalledTimes(1);
    expect(onSearch).toHaveBeenCalledWith('wid', expect.anything());
  });

  it('ignores a stale response that resolves after a newer one', async () => {
    const first = deferred();
    const second = deferred();
    const onSearch = vi
      .fn()
      .mockImplementationOnce(() => first.promise)
      .mockImplementationOnce(() => second.promise);

    render(<SearchBox onSearch={onSearch} />);
    const input = screen.getByPlaceholderText(/search/i);

    fireEvent.change(input, { target: { value: 'a' } });
    await act(async () => {
      vi.advanceTimersByTime(300);
    });

    fireEvent.change(input, { target: { value: 'ab' } });
    await act(async () => {
      vi.advanceTimersByTime(300);
    });

    // Second (newer) request resolves first; the stale first request
    // resolves late and must not clobber the screen.
    await act(async () => {
      second.resolve([{ id: 2, name: 'Ab result' }]);
      await Promise.resolve();
    });
    await act(async () => {
      first.resolve([{ id: 1, name: 'Stale a result' }]);
      await Promise.resolve();
    });

    expect(screen.queryByText('Stale a result')).not.toBeInTheDocument();
    expect(screen.getByText('Ab result')).toBeInTheDocument();
  });

  it('clears results immediately when the query is emptied, without a network call', async () => {
    const onSearch = vi.fn().mockResolvedValue([{ id: 1, name: 'Widget' }]);
    render(<SearchBox onSearch={onSearch} />);
    const input = screen.getByPlaceholderText(/search/i);

    fireEvent.change(input, { target: { value: 'w' } });
    await act(async () => {
      vi.advanceTimersByTime(300);
      await Promise.resolve();
    });
    expect(screen.getByText('Widget')).toBeInTheDocument();

    const callsBeforeClear = onSearch.mock.calls.length;
    fireEvent.change(input, { target: { value: '' } });
    await act(async () => {
      vi.advanceTimersByTime(300);
    });

    expect(screen.queryByText('Widget')).not.toBeInTheDocument();
    expect(onSearch.mock.calls.length).toBe(callsBeforeClear);
  });

  it('shows an error state when the search rejects', async () => {
    const onSearch = vi.fn().mockRejectedValue(new Error('network down'));
    render(<SearchBox onSearch={onSearch} />);
    const input = screen.getByPlaceholderText(/search/i);

    fireEvent.change(input, { target: { value: 'w' } });
    await act(async () => {
      vi.advanceTimersByTime(300);
      await Promise.resolve();
    });

    expect(screen.getByRole('alert')).toBeInTheDocument();
  });

  it('does not update state (or warn) after unmounting mid-request', async () => {
    const pending = deferred();
    const onSearch = vi.fn().mockReturnValue(pending.promise);
    const errorSpy = vi.spyOn(console, 'error').mockImplementation(() => {});

    const { unmount } = render(<SearchBox onSearch={onSearch} />);
    const input = screen.getByPlaceholderText(/search/i);
    fireEvent.change(input, { target: { value: 'w' } });
    await act(async () => {
      vi.advanceTimersByTime(300);
    });

    unmount();
    await act(async () => {
      pending.resolve([{ id: 1, name: 'Too late' }]);
      await Promise.resolve();
    });

    const reactWarning = errorSpy.mock.calls.some((args) =>
      String(args[0]).includes('state update on an unmounted component')
    );
    expect(reactWarning).toBe(false);
    errorSpy.mockRestore();
  });
});
