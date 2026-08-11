export async function getDominantColors(imageSrc, colorCount = 3) {
    return new Promise((resolve, reject) => {
        const img = new Image();

        img.crossOrigin = "Anonymous";

        img.onload = () => {
            const canvas = document.createElement("canvas");
            const ctx = canvas.getContext("2d", { willReadFrequently: true });

            if (!ctx) {
                reject(new Error("Canvas context unavailable"));
                return;
            }

            const size = 100;

            canvas.width = size;
            canvas.height = size;

            ctx.drawImage(img, 0, 0, size, size);

            const imageData = ctx.getImageData(
                0,
                0,
                size,
                size
            ).data;

            const colors = [];

            for (let i = 0; i < imageData.length; i += 4) {
                const r = imageData[i];
                const g = imageData[i + 1];
                const b = imageData[i + 2];
                const a = imageData[i + 3];

                if (a < 128) continue;

                colors.push([r, g, b]);
            }

            const groups = [];

            for (const color of colors) {
                let found = false;

                for (const group of groups) {
                    const distance =
                        Math.abs(color[0] - group.color[0]) +
                        Math.abs(color[1] - group.color[1]) +
                        Math.abs(color[2] - group.color[2]);

                    if (distance < 60) {
                        group.count++;
                        found = true;
                        break;
                    }
                }

                if (!found) {
                    groups.push({
                        color,
                        count: 1
                    });
                }
            }

            groups.sort((a, b) => b.count - a.count);

            const result = groups
                .slice(0, colorCount)
                .map(group => {
                    const [r, g, b] = group.color;

                    return `rgb(${r}, ${g}, ${b})`;
                });

            resolve(result);
        };

        img.onerror = reject;

        img.src = imageSrc;
    });
}