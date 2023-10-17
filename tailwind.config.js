/** @type {import('tailwindcss').Config} */
module.exports = {
  content: [
    './Pages/**/*.cshtml',
    './Views/**/*.cshtml'
  ],
  theme: {
    extend: {
      colors:{
        "black-s": "#333333"
      }
    },
  },
  plugins: [],
}

