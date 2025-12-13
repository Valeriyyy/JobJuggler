<script lang="ts">
    import Layout from "../layouts/Layout.svelte";
    import type { Client } from "../lib/models/types";
    import SomeDataDisplayComponent from "../lib/components/SomeDataDisplayComponent.svelte";
    
    interface Props {
        client: Client;
        defaultData?: string;
    }
    
    let { client, defaultData = "Default Data" }: Props = $props();
</script>

<Layout title="Data Display">
    <h1>Client Information</h1>
    
    <SomeDataDisplayComponent client={client} />
</Layout>

