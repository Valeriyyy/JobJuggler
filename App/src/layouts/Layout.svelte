<script lang="ts">
    import { onMount } from "svelte";
    import "bootstrap/dist/css/bootstrap.min.css";

    interface Props {
        title?: string;
        children?: any;
    }
    
    let {title = "JobJuggler", children}: Props = $props();
    let currentPath = $state("/");
    let navbarExpanded = $state(false);
    
    onMount(() => {
        currentPath = window.location.pathname;
    });
    
    function isActive(path: string): boolean {
        return currentPath === path || currentPath.startsWith(path + "/");
    }
    
    function toggleNavbar() {
        navbarExpanded = !navbarExpanded;
    }
    
    function closeNavbar() {
        navbarExpanded = false;
    }
</script>

<svelte:head>
    <title>{title} - JobJuggler</title>
</svelte:head>

<div class="app-layout">
    <header>
        <nav>
            <div class="container-fluid">
                <a href="/" class="navbar-brand">JobJuggler</a>
                <button class="navbar-toggler" type="button"
                        onclick={toggleNavbar}
                        aria-controls="navbarNav" aria-expanded={navbarExpanded} aria-label="Toggle navigation">
                    <span class="navbar-toggler-icon"></span>
                </button>
                <div class="collaps navbar-collapse" class:show={navbarExpanded} id="navbarNav">
                    <ul class="navbar-nav me-auto mb-2 mb-lg-0">
                        <li class="nav-item">
                            <a href="/" class="nav-link" class:active={isActive("/")} onclick={closeNavbar}>Home</a>
                        </li>
                        <li class="nav-item">
                            <a href="/Home/Privacy" class="nav-link" class:active={isActive("/Home/Privacy")} onclick={closeNavbar}>Privacy</a>
                        </li>
                        <li class="nav-item">
                            <a href="/Home/SomeDataDisplay" class="nav-link" 
                               class:active={isActive("/Home/SomeDataDisplay")} 
                               onclick={closeNavbar}>Data Display</a>
                        </li>
                    </ul>
                </div>
            </div>
        </nav>
    </header>
    
    <div class="container">
        <div class="pb-3">
            {@render children?.()}
        </div>
    </div>
    
    <div class="border-top footer text-muted">
        <div class="container">
            @ {new Date().getFullYear()} - JobJuggler - <a href="/Home/Privacy">Privacy Policy</a>
        </div>
    </div>
</div>

<style>
    .app-layout {
        min-height: 100vh;
        display: flex;
        flex-direction: column;
    }
    
    .container {
        flex: 1;
    }
    
    .footer {
        padding: 1rem 0;
        margin-top: 2rem;
    }
    
    .nav-link.active {
        color: aquamarine !important;
        font-weight: 600;
    }
</style>
