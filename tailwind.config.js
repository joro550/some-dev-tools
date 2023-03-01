/** @type {import('tailwindcss').Config} */
module.exports = {
  content:
      [
        "./Client/Pages/**/*.{razor,cshtml}",
        "./Client/Shared/**/*.{razor,cshtml}"
      ],
  theme: {
    extend: {},
  },
  plugins: [require('@tailwindcss/typography')],
}
