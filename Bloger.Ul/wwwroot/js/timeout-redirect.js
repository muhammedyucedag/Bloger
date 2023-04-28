(function () {
    const idleDurationSecs = 60; // 5 dakika, oturum süresi (saniye cinsinden)
    const redirectUrl = '/Login/LogOff'; // Yönlendirilecek URL
    let idleTimeout; // Zamanlayıcı fonksiyonu

    // Zamanlayıcı fonksiyonunu başlatın
    const resetIdleTimeout = function () {
        // Mevcut zamanlayıcıyı sıfırlayın
        if (idleTimeout) clearTimeout(idleTimeout);

        // Yeni bir zamanlayıcı belirleyin
        idleTimeout = setTimeout(() => location.href = redirectUrl, idleDurationSecs * 1000);
    };

    // Sayfa yüklenirken zamanlayıcıyı başlatın
    resetIdleTimeout();

    // Zamanlayıcıyı sıfırlayacak olayları belirleyin
    ['click', 'touchstart', 'mousemove'].forEach(evt =>
        document.addEventListener(evt, resetIdleTimeout, false)
    );
})();