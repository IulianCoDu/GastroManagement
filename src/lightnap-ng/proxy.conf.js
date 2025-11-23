const PROXY_CONFIG = {
    "/api": {
        target: "https://localhost:44352",
        secure: false,
        changeOrigin: true,
        ws: true,
        logLevel: "debug",
    },
};

module.exports = PROXY_CONFIG;
