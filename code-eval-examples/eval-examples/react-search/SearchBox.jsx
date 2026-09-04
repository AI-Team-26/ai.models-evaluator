import { useState } from 'react';

export default function SearchBox({ onSearch }) {
  const [query, setQuery] = useState('');
  const [results, setResults] = useState([]);
  const [status, setStatus] = useState('idle'); // idle | loading | success | error
  const [error, setError] = useState(null);

  function handleChange(e) {
    setQuery(e.target.value);
    // TODO: debounce calling onSearch(query), avoid races between fast
    // successive keystrokes, clear results when the query is empty, and
    // clean up properly if the component unmounts mid-request.
  }

  return (
    <div>
      <input value={query} onChange={handleChange} placeholder="Search products..." />
      {status === 'loading' && <p>Loading…</p>}
      {status === 'error' && <p role="alert">{error}</p>}
      <ul>
        {results.map((r) => (
          <li key={r.id}>{r.name}</li>
        ))}
      </ul>
    </div>
  );
}
