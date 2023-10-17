/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml'
    ],
    theme: {
        extend: {
            colors: {
                "black-s": "#333333"
            },
            gridTemplateRows: {
                '6': 'repeat(6, minmax(0, 1fr))',
            }
        },
    },
    plugins: [],
}

