/** @type {import('tailwindcss').Config} */
module.exports = {
    content: [
        './Pages/**/*.cshtml',
        './Views/**/*.cshtml'
    ],
    theme: {
        extend: {
            colors: {
                "cus-slate": "#F8FBFD"
            },
            gridTemplateRows: {
                '6': 'repeat(6, minmax(0, 1fr))',
            }
        },
    },
    plugins: [],
}

