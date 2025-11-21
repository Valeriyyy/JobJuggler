This README explains how to build and run the API + nginx + Postgres with Docker Compose for local development.

Overview
- The compose file defines three services: jobjuggler_api (the ASP.NET app), db (Postgres), and nginx (TLS termination + reverse proxy).
- nginx terminates TLS for hostname `localhost.jobjuggler.com` and proxies to the API over the compose network on port 8080.

Ports
- Host ports mapped by Compose:
  - API HTTP: 8080 -> container 8080
  - API HTTPS (Kestrel): 5001 -> container 5001
  - nginx HTTPS: 443 -> container 443

Why both 8080 and 5001?
- The app is configured to listen on 8080 (HTTP) and 5001 (HTTPS). nginx is configured to terminate TLS and forward to the API's HTTP endpoint (8080). If you prefer to let nginx handle all TLS and keep the app HTTP-only, set the ASPNETCORE_URLS to "http://+:8080" in the container environment or Dockerfile.

Certificates
- The repo contains `nginx/certs` with a self-signed certificate `localhost.jobjuggler.com.crt`/`.key` used by nginx. Browsers won't trust it by default; to use it without trusting it system-wide, you'll see certificate warnings. For local development you can:
  - Add the self-signed cert to your OS/browser trust store (preferred), or
  - Use the app's built-in dev certificate (the Dockerfile generates one for Kestrel), or
  - Use mkcert or your OS's dev cert tooling to issue a cert trusted by your host.

Should `nginx/` be committed?
- Yes: commit `nginx/conf.d/default.conf` to keep the proxy configuration consistent across environments.
- For `nginx/certs`, committing is optional. If the certs are only for local development and not sensitive, it's convenient to include them. If they're private, omit them and document how to create them instead.

Using the compose file on another computer
1. Clone the repo on the other machine.
2. Ensure Docker and Docker Compose v2 (docker compose) are installed.
3. If you committed `nginx/certs`, you're ready. If not, create or copy `nginx/certs/localhost.jobjuggler.com.crt` and `.key` into `JobJuggler.API/nginx/certs`.
4. Add a hosts entry mapping `localhost.jobjuggler.com` to 127.0.0.1 (requires admin privileges):

   # macOS / Linux (add to /etc/hosts)
   127.0.0.1   localhost.jobjuggler.com

5. Run from the repo root:

```bash
# Build and bring up the services
docker compose -f JobJuggler.API/docker-compose.yml up --build
```

Notes on the Dockerfile build failure you saw (missing .csproj path)
- The Dockerfile copies project files using relative paths from the build context. The compose file's `build.context` must be the repository root (where the other project folders live). The compose file has been updated to set `context: ..` when invoked from `JobJuggler.API` so those paths resolve.

Troubleshooting: nginx 502 / host not found
- 502 Bad Gateway: usually means nginx couldn't connect to the API (app not started, wrong port, or service name mismatch). The compose file uses the service name `jobjuggler_api` and nginx uses `http://jobjuggler_api:8080/` as upstream. If nginx reports `host not found in upstream` for `jobjuggler.api` it means the upstream name didn't match any container/network alias.
- If you want to use the alternative name `jobjuggler.api`, add it to `networks.aliases` on the API service in `docker-compose.yml` (both are fine).

Kestrel cert permissions
- If you receive AccessDenied when Kestrel tries to read `/https/aspnetapp.pfx`, ensure the Dockerfile sets proper permissions. The Dockerfile in this repo copies the generated file and sets permissive read perms with `chmod 0644`.

If you want, I can:
- Add an alias `jobjuggler.api` to the API service so the nginx config can use that name (instead of `jobjuggler_api`).
- Change nginx to proxy to `http://jobjuggler_api:8080/` (already set).
- Adjust Dockerfile to only bind HTTP when behind nginx.


