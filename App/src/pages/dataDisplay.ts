import { mount} from "svelte";
import "../assets/css/app.css";

import Layout from "../layouts/Layout.svelte";
import type { Client } from "../lib/models/types";
import DataDisplay from "./DataDisplay.svelte";

const appElement = document.getElementById("data-display-app")!;

const clientData: Client = JSON.parse(appElement?.dataset.client || "{}");
const defaultValue = appElement?.dataset.defaultValue || "N/A";

const app = mount(DataDisplay, {
    target: appElement,
    props: {
        client: clientData,
        defaultValue
    }
});

export default app;