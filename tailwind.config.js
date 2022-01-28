module.exports = {
    mode: 'jit',
    purge: [
        './Student/*.aspx',
        './Librarian/*.aspx',
        './Student/*.aspx.cs',
    ],
    darkMode: false,
    theme: {
        extend: {
            colors: {
                'custom-darkblue': '#022859',
                'custom-ashblue': '#657A8C',
                'custom-darkgray':'#a6a6a6',
                'custom-lightgray': '#F2F2F2',
                'custom-black': '#0d0d0d',
                'custom-red': '#e74141',
                'custom-orange': '#E4984E',
                'custom-yellow': '#e2e140',
                'custom-lightgreen': '#73df44',
                'custom-green':'#46dd60'
            },
            fontFamily: {
                sans: ['Nunito', 'sans-serif'],
            },
        },
    },
    variants: {
        extend: {},
    },
    plugins: [],
}