/** @type {import('tailwindcss').Config} */
export default {
  content: [
    "./index.html",
    "./src/**/*.{js,ts,jsx,tsx}",
  ],
  theme: {
    extend: {
      fontFamily: {
        sans: ['Nunito', 'sans-serif'],
        serif: ['Georgia', 'Cambria', 'serif'],
        mono: ['JetBrains Mono', 'monospace'],
      },
      colors: {
        primary: {
          DEFAULT: '#58CC02',
          dark: '#46A301',
        },
        secondary: {
          DEFAULT: '#1CB0F6',
          dark: '#0A8CCF',
        },
        accent: {
          DEFAULT: '#FF9600',
        },
        danger: {
          DEFAULT: '#FF4B4B',
        },
        warning: {
          DEFAULT: '#FFC800',
        },
        neutral: {
          50: '#F7F7F7',
          100: '#E5E5E5',
          200: '#D4D4D4',
          300: '#B4B4B4',
          400: '#999999',
          500: '#787878',
          600: '#5E5E5E',
          700: '#4A4A4A',
          800: '#2F2F2F',
          900: '#1A1A1A',
        },
        white: '#FFFFFF',
        black: '#000000',
      },
      boxShadow: {
        'duo-bottom': '0 4px 0 #00000015',
        'duo-button': '0 4px 0',
        'duo-button-pressed': '0 2px 0',
      },
    },
  },
  plugins: [],
}
