import { FormEvent, useState } from 'react'
import { RegisterResponse, Session } from '../types'

const IDENTITY_URL = import.meta.env.VITE_IDENTITY_URL ?? 'http://localhost:8091'

function RegisterForm({ onRegistered }: { onRegistered: (session: Session, token: string) => void }) {
  const [username, setUsername] = useState('')
  const [email, setEmail] = useState('')
  const [password, setPassword] = useState('')
  const [error, setError] = useState('')
  const [busy, setBusy] = useState(false)

  const handleSubmit = async (event: FormEvent) => {
    event.preventDefault()
    setError('')
    setBusy(true)
    try {
      const response = await fetch(`${IDENTITY_URL}/register`, {
        method: 'POST',
        headers: { 'Content-Type': 'application/json' },
        body: JSON.stringify({ username, email, password }),
      })
      if (response.ok) {
        const data: RegisterResponse = await response.json()
        onRegistered({ username: data.username, email: data.email }, data.token)
      } else {
        const body = await response.json().catch(() => ({}))
        setError(body.error ?? `Request failed (${response.status})`)
      }
    } catch {
      setError('Cannot reach the identity service.')
    } finally {
      setBusy(false)
    }
  }

  return (
    <form className="card" onSubmit={handleSubmit}>
      <label>
        Username
        <input
          value={username}
          minLength={3}
          maxLength={32}
          onChange={e => setUsername(e.target.value)}
          required
        />
      </label>
      <label>
        Email
        <input type="email" value={email} onChange={e => setEmail(e.target.value)} required />
      </label>
      <label>
        Password
        <input
          type="password"
          value={password}
          minLength={8}
          onChange={e => setPassword(e.target.value)}
          required
        />
      </label>
      {error && <p className="error">{error}</p>}
      <button type="submit" disabled={busy}>{busy ? 'Registering…' : 'Register'}</button>
    </form>
  )
}

export default RegisterForm
