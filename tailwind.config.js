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
                'darkblue': '#022859',
                'ashblue': '#657A8C',
                'darkgray':'#a6a6a6',
                'lightgray': '#F2F2F2',
                'black': '#0d0d0d',
                'red': '#e74141',
                'orange': '#E4984E',
                'yellow': '#e2e140',
                'lightgreen': '#73df44',
                'green':'#46dd60'
            }
        },
    },
    variants: {
        extend: {},
    },
    plugins: [],
}