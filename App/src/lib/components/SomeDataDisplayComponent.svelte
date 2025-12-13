<script lang="ts">
    import type { Client } from "../models/types";
    
    interface Props {
        client: Client;
    }
    
    let { client }: Props = $props();
    let currentClient = $state<Client>(client);
    let loading = $state<boolean>(false);
    let error = $state<string | null>(null);
    
    async function getNextClient() {
        loading = true;
        error = null;
        
        try {
            const nextClientId = currentClient.id + 1;
            const response = await fetch(`/${nextClientId}`);
            if (!response.ok) {
                error = "Http error " + response.status;
                return;
            }
            currentClient = await response.json();
        } catch (err) {
            error = err instanceof Error ? err.message : "Failed to fetch client data";
            console.error(err);
        } finally {
            loading = false;
        }
    }
</script>

{#if loading}
    <p>Loading client data...</p>
{:else if error}
    <p class="error">Error: {error}</p>
{:else}
    <div class="client-info">
        <h2>{currentClient.id} - {currentClient.name}</h2>
        <p>Email: {currentClient.email}</p>
        <p>Phone: {currentClient.phone}</p>
        <!-- Add more client details as needed -->
        <div class="mt-4 text-center">
            <button class="btn btn-primary"
            onclick={getNextClient}
            disabled={loading}>
                {loading ? "loading..." : "Load Next Client"}
            </button>
        </div>
    </div>
{/if}