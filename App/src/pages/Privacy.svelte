<script lang="ts">
    import Layout from "../layouts/Layout.svelte";
    const privacyKey = "privacy-accepted";
    
    let privacyAccepted = $state(false);
    
    function approvePrivacy() {
        privacyAccepted = true;
        localStorage.setItem(privacyKey, "true");
    }
    
    $effect(() => {
        privacyAccepted = localStorage.getItem(privacyKey) === "true";
    });
</script>

<Layout title="Privacy Policy">
    <main class="privacy-policy">
        <h1>Privacy Policy</h1>
        <p>Your privacy is important to us. This privacy policy explains how we collect, use, and protect your information when you use our application.</p>
        
        <h2>Information We Collect</h2>
        <ul>
            <li><strong>Personal Information:</strong> We do not collect any personal information such as your name, email address, or phone number.</li>
            <li><strong>Usage Data:</strong> We may collect non-personal information about how you interact with our application, such as the pages you visit and the features you use.</li>
        </ul>
        
        <h2>How We Use Your Information</h2>
        <ul>
            <li>To improve our application and provide better services.</li>
            <li>To analyze usage patterns and trends.</li>
        </ul>
        
        <h2>Data Security</h2>
        <p>We implement appropriate security measures to protect your information from unauthorized access, alteration, disclosure, or destruction.</p>
        
        <h2>Your Consent</h2>
        {#if !privacyAccepted}
            <p>By using our application, you consent to our privacy policy. Please click the button below to acknowledge that you have read and understood our privacy policy.</p>
            <button onclick={approvePrivacy}>I Accept</button>
        {:else}
            <p>Thank you for accepting our privacy policy.</p>
        {/if}
    </main>
</Layout>