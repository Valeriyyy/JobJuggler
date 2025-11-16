# Docker Setup Guide

## Overview

This project uses Docker Compose to run:
- **API** (ASP.NET 9.0) on `http://localhost:8080` and `https://localhost:5001`
- **Nginx** reverse proxy on `https://localhost.jobjuggler.com`
- **PostgreSQL** database

## Prerequisites

- Docker and Docker Compose installed
- OpenSSL (usually pre-installed on macOS)
- Administrator/sudo access (to add host entry and trust certificate)

## First-Time Setup

### 1. Generate Self-Signed Certificate

Run this command from the repository root:

```bash
mkdir -p JobJuggler.API/nginx/certs
openssl req -x509 -nodes -days 365 -newkey rsa:2048 \
  -keyout JobJuggler.API/nginx/certs/localhost.jobjuggler.com.key \
  -out JobJuggler.API/nginx/certs/localhost.jobjuggler.com.crt \
  -subj "/CN=localhost.jobjuggler.com"
```

### 2. Add Host Entry

Edit `/etc/hosts` (requires sudo):

```bash
sudo nano /etc/hosts
```

Add this line:

```
127.0.0.1 localhost.jobjuggler.com
```

Save and exit (Ctrl+X, then Y, then Enter in nano).

### 3. Trust the Certificate on macOS (Optional but Recommended)

This prevents browser warnings when accessing the app:

```bash
sudo security add-trusted-cert -d -r trustRoot -k /Library/Keychains/System.keychain JobJuggler.API/nginx/certs/localhost.jobjuggler.com.crt
```

You may need to restart your browser after running this command.

## Running the Application

### Start the Docker Compose Stack

From the repository root:

```bash
docker compose -f JobJuggler.API/docker-compose.yml up --build
```

The `--build` flag rebuilds the API image. Omit it on subsequent runs if no code changes were made.

### Access the Application

- **API (direct):** `http://localhost:8080`
- **API (HTTPS direct):** `https://localhost:5001`
- **Nginx Proxy:** `https://localhost.jobjuggler.com`
- **API Documentation (Scalar):** `https://localhost.jobjuggler.com/scalar/v1`

### Stop the Stack

Press `Ctrl+C` in the terminal, or in another terminal:

```bash
docker compose -f JobJuggler.API/docker-compose.yml down
```

## Troubleshooting

### Certificate Not Trusted in Browser

If you see a certificate warning in your browser:

1. Verify you ran the macOS trust command (step 3 above).
2. Clear your browser cache (especially HSTS for the domain).
3. Restart your browser completely.
4. If still not working, you can bypass the warning and accept the certificate once (usually a button on the error page).

### Can't Connect to `localhost.jobjuggler.com`

1. Verify the host entry was added to `/etc/hosts`:
   ```bash
   cat /etc/hosts | grep jobjuggler
   ```
   Should print: `127.0.0.1 localhost.jobjuggler.com`

2. Flush your DNS cache (macOS):
   ```bash
   sudo dscacheutil -flushcache
   ```

### Port Already in Use

If port 443, 8080, 5001, or 5432 is in use:

1. Find the process using the port:
   ```bash
   lsof -i :PORT_NUMBER
   ```

2. Either stop that process or update the port mappings in `docker-compose.yml`.

### Database Connection Issues

Ensure PostgreSQL is running and healthy:

```bash
docker compose -f JobJuggler.API/docker-compose.yml ps
```

The `postgres` service should show status `healthy` after a few seconds.

## Running on a Different Computer

1. Clone the repository.
2. Follow **First-Time Setup** steps (1–3 above).
3. Run the application using the command in the **Running the Application** section.

## Notes

- The `nginx/certs/` directory is **not** committed to source control. Each machine generates its own certificate.
- The `nginx/conf.d/default.conf` file is committed and shared across all machines.
- The API is set to listen on `http://+:8080` internally; Nginx terminates TLS on port 443 (host) and proxies to the API over HTTP.

