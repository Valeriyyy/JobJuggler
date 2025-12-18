<script lang="ts">
    import Layout from "../layouts/Layout.svelte";
    import type { CompanyDTO } from "../lib/models/types";
    import SomeDataDisplayComponent from "../lib/components/SomeDataDisplayComponent.svelte";

    interface Props {
        company: CompanyDTO;
        defaultData?: string;
    }
    
    let { company, defaultData = "Default Data" }: Props = $props();
</script>

<Layout title="Data Display">
    <h1>Client Information</h1>
    
    <SomeDataDisplayComponent company={company} />
</Layout>

