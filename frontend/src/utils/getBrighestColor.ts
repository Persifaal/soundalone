export function getBrightestColor(
    imageSrc: string
): Promise<string> {

    return new Promise((resolve, reject) => {

        const img = new Image();

        img.crossOrigin = "Anonymous";

        img.onload = () => {

            const canvas = document.createElement("canvas");
            const ctx = canvas.getContext("2d");

            if (!ctx) {
                reject(new Error("Canvas context unavailable"));
                return;
            }

            // Уменьшаем изображение для производительности
            const size = 100;

            canvas.width = size;
            canvas.height = size;

            ctx.drawImage(
                img,
                0,
                0,
                size,
                size
            );

            const imageData = ctx.getImageData(
                0,
                0,
                size,
                size
            );

            const pixels = imageData.data;

            let bestColor = {
                r: 0,
                g: 0,
                b: 0
            };

            let bestScore = -1;


            for (let i = 0; i < pixels.length; i += 4) {

                const r = pixels[i];
                const g = pixels[i + 1];
                const b = pixels[i + 2];
                const a = pixels[i + 3];

                // Игнорируем прозрачные пиксели
                if (a < 128) {
                    continue;
                }

                const max = Math.max(r, g, b);
                const min = Math.min(r, g, b);

                // Яркость
                const brightness = max / 255;

                // Насыщенность
                const saturation =
                    max === 0
                        ? 0
                        : (max - min) / max;


                /*
                 * Чем выше:
                 *
                 * brightness
                 * saturation
                 *
                 * тем выше вероятность
                 * выбора цвета.
                 */
                const score =
                    brightness * 0.6 +
                    saturation * 0.4;


                if (score > bestScore) {

                    bestScore = score;

                    bestColor = {
                        r,
                        g,
                        b
                    };
                }
            }


            resolve(
                `rgb(${bestColor.r}, ${bestColor.g}, ${bestColor.b})`
            );
        };


        img.onerror = () => {
            reject(
                new Error("Failed to load image")
            );
        };


        img.src = imageSrc;
    });
}