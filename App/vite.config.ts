import { defineConfig } from 'vite'
import { svelte } from '@sveltejs/vite-plugin-svelte'
import { resolve } from "path";

// https://vite.dev/config/
export default defineConfig({
    plugins: [svelte()],
    build: {
        outDir: '../JobJuggler.API/wwwroot/dist',   // ← IMPORTANT
        emptyOutDir: true,
        manifest: true,
        rollupOptions: {
            input: {
                app: resolve(__dirname, 'src/pages/app.ts')
            }
        }
    },
    server: {
        strictPort: true,
        port: 5173,
        origin: 'http://localhost:5173'
    }
})
