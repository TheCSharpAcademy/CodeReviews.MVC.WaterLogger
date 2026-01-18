const themeKey = "theme";
const switchButton = document.getElementById("theme-switch");

const storedTheme = localStorage.getItem(themeKey);
const systemPrefersDark = window.matchMedia("(prefers-color-scheme: dark)").matches;

const initialTheme = storedTheme || (systemPrefersDark ? "dark" : "light");

document.documentElement.setAttribute("data-bs-theme", initialTheme);
switchButton.checked = initialTheme === "dark";

switchButton.addEventListener("change", () => {
  const theme = switchButton.checked ? "dark" : "light";
  document.documentElement.setAttribute("data-bs-theme", theme);
  localStorage.setItem(themeKey, theme);
});

window.matchMedia("(prefers-color-scheme: dark)").addEventListener("change", (e) => {
  if (!localStorage.getItem(themeKey)) {
    const theme = e.matches ? "dark" : "light";
    document.documentElement.setAttribute("data-bs-theme", theme);
    switchButton.checked = theme === "dark";
  }
});
