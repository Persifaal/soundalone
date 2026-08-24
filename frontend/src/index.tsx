import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";

import Main from "./pages/Main";
import RegisterPage from "./pages/Register";
import LoginPage from "./pages/Login";
import ResetPasswordPage from "./pages/ResetPassword";
import Confirm from "./pages/Confirm";

import { AuthProvider } from "./Context/AuthContext";
import { PlayerProvider } from "./Context/PlayerContext";

const router = createBrowserRouter([
    {
        path: "/",
        element: <Main />
    },
    {
        path: "/auth/register",
        element: <RegisterPage />
    },
    {
        path: "/auth/login",
        element: <LoginPage />
    },
    {
        path: "/auth/reset-password",
        element: <ResetPasswordPage />
    },
    {
        path: "/auth/confirm",
        element: <Confirm />
    }
]);

const rootElement = document.getElementById("root")!;

createRoot(rootElement).render(
    <AuthProvider>
        <PlayerProvider>
            <RouterProvider router={router} />
        </PlayerProvider>
    </AuthProvider>
);