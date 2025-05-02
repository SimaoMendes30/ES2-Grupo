window.applyCssVarsFromTheme = function (theme) {
    const root = document.documentElement;

    if (theme.primary)
        root.style.setProperty('--app-primary', theme.primary);

    if (theme.secondary)
        root.style.setProperty('--app-secondary', theme.secondary);

    if (theme.radius)
        root.style.setProperty('--app-radius', theme.radius);
};
