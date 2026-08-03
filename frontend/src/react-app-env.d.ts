/* eslint-disable react/no-typos */
import 'react';

declare module 'react' {
    interface CSSProperties {
        [key: `--${string}`]: string | number;
    }
}
declare module '*.png' {
    const value: string;
    export default value;
}

declare module '*.jpg' {
    const value: string;
    export default value;
}

declare module '*.jpeg' {
    const value: string;
    export default value;
}

declare module '*.gif' {
    const value: string;
    export default value;
}

declare module '*.svg' {
    const value: string;
    export default value;
}

declare module '*.webp' {
    const value: string;
    export default value;
}

// Объявления для CSS-переменных в inline-стилях
import 'react';

declare module 'react' {
    interface CSSProperties {
        [key: `--${string}`]: string | number;
    }
}