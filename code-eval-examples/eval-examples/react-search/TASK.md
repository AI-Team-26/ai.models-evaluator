# Task: SearchBox component

Implement `SearchBox.jsx`. It receives one prop, `onSearch(query, signal)`,
an async function that resolves to an array of `{ id, name }` results.

Requirements:
- As the user types, search — but don't call `onSearch` on every keystroke.
- Show a loading state while a search is in flight, and an error state if
  `onSearch` rejects (render the error with `role="alert"`).
- When the input is cleared, the result list should clear too.
- The component will be mounted and unmounted repeatedly inside a larger
  page (e.g. a modal). Think about what that implies.
- Users type at different speeds and sometimes edit their query while a
  previous search is still pending — think about what should happen to the
  screen in that case.

Only `SearchBox.jsx` needs to change.
