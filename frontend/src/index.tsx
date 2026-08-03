import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Main from "./Main"

const router = createBrowserRouter([
    { path: "/", element: <Main /> },
]);

const rootElement = document.getElementById("root")!;
createRoot(rootElement).render(<RouterProvider router={router} />);