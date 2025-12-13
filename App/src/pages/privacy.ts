import { mount } from "svelte";
import "../assets/css/app.css";
import Privacy from "./Privacy.svelte";

const app = mount(Privacy, {
    target: document.getElementById("privacy-app")!,
});

export default app;