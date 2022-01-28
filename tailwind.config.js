module.exports = {
    mode: 'jit',
    purge: [
        './Student/*.aspx',
        './Librarian/*.aspx',
    ],
    darkMode: false,
    theme: {
        extend: {
            colors: {
                'ashblue': '#657A8C',
                'lightgray': '#F2F2F2',
                'orange':'#E4984E',
            }
        },
    },
    variants: {
        extend: {},
    },
    plugins: [],
}