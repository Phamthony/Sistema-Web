function descargarPDF() {
    const element = document.getElementById('resultadoNomina');

    if (!element) {
        alert("No hay contenido para exportar.");
        return;
    }

    const opt = {
        margin: 0.5,
        filename: 'nomina_diaria.pdf',
        image: { type: 'jpeg', quality: 0.98 },
        html2canvas: { scale: 2 },
        jsPDF: { unit: 'in', format: 'letter', orientation: 'portrait' }
    };

    html2pdf().set(opt).from(element).save();
}