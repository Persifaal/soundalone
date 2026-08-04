import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Main from "./pages/Main"
import RegisterPage from "./pages/Register"
import LoginPage from "./pages/Login";
import ResetPasswordPage from "./pages/ResetPassword";

const router = createBrowserRouter([
    { path: "/", element: <Main /> },
    { path: "/register", element: <RegisterPage /> },
    { path: "/login", element: <LoginPage /> },
    { path: "/reset-password", element: <ResetPasswordPage /> },
]);

const rootElement = document.getElementById("root")!;
createRoot(rootElement).render(<RouterProvider router={router} />);