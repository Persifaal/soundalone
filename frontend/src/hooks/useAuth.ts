import { useEffect, useState } from "react";
import Cookies from "universal-cookie";

export function useAuth() {
    const [isLoggedIn, setIsLoggedIn] = useState(false);

    useEffect(() => {
        const cookies = new Cookies();
        setIsLoggedIn(!!cookies.get("auth-token"));
    }, []);

    return isLoggedIn;
}