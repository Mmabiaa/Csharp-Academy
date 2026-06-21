import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import { QueryClient, QueryClientProvider } from '@tanstack/react-query'
import { AuthProvider } from './context/AuthContext'
import { NotificationProvider } from './context/NotificationContext'
import { SoundProvider } from './context/SoundContext'
import './index.css'
import App from './App.tsx'

const queryClient = new QueryClient()

createRoot(document.getElementById('root')!).render(
  <StrictMode>
    <QueryClientProvider client={queryClient}>
      <AuthProvider>
        <SoundProvider>
          <NotificationProvider>
            <App />
          </NotificationProvider>
        </SoundProvider>
      </AuthProvider>
    </QueryClientProvider>
  </StrictMode>,
)
