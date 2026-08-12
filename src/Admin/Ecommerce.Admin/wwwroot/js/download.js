window.downloadFile = (fileName, contentType, bytes) => {
    // bytes уже является Uint8Array, дополнительное оборачивание не требуется
    const blob = new Blob([bytes], { type: contentType });

    const url = URL.createObjectURL(blob);

    const link = document.createElement("a");
    link.href = url;
    link.download = fileName;

    document.body.appendChild(link);
    link.click();
    link.remove();

    URL.revokeObjectURL(url);
};