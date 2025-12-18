<script lang="ts">
    import type { CompanyDTO } from "../models/types";

    interface Props {
        company: CompanyDTO;
    }
    
    let { company }: Props = $props();
    let currentCompany = $state<CompanyDTO>(company);
    let loading = $state<boolean>(false);
    let error = $state<string | null>(null);
    
    async function getNextCompany() {
        loading = true;
        error = null;
        
        try {
            const nextCompanyId = currentCompany.id + 1;
            const response = await fetch(`/${nextCompanyId}`);
            if (!response.ok) {
                error = "Http error " + response.status;
                return;
            }
            currentCompany = await response.json();
        } catch (err) {
            error = err instanceof Error ? err.message : "Failed to fetch company data";
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
        <h2>{currentCompany.id} - {currentCompany.name}</h2>
        <p>Email: {currentCompany.mainContactName}</p>
        <p>Email: {currentCompany.mainContactEmail}</p>
        <p>Phone: {currentCompany.mainContactPhone}</p>
        <!-- Add more client details as needed -->
        <div class="mt-4 text-center">
            <button class="btn btn-primary"
            onclick={getNextCompany}
            disabled={loading}>
                {loading ? "loading..." : "Load Next Client"}
            </button>
        </div>
    </div>
{/if}