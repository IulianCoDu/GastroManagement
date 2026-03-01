const PROXY_CONFIG = {
  "/api": {
    target: "https://gastromanagement.up.railway.app",
    secure: true,
    changeOrigin: true,
    ws: true,
  }
};

module.exports = PROXY_CONFIG;
