import { createRoot } from "react-dom/client";
import { createBrowserRouter, RouterProvider } from "react-router-dom";
import Main from "./pages/Main"
import RegisterPage from "./pages/Register"
import LoginPage from "./pages/Login";
import ResetPasswordPage from "./pages/ResetPassword";
import MainLogged from "./pages/MainLogged";

const router = createBrowserRouter([
    { path: "/", element: <Main /> },
    { path: "/register", element: <RegisterPage /> },
    { path: "/login", element: <LoginPage /> },
    { path: "/reset-password", element: <ResetPasswordPage /> },
    { path: "/main", element: <MainLogged /> },
]);

const rootElement = document.getElementById("root")!;
createRoot(rootElement).render(<RouterProvider router={router} />);