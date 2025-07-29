import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.scss'
import * as bootstrap from 'bootstrap'
import App from './App.tsx'

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <App />
  </StrictMode>,
)
