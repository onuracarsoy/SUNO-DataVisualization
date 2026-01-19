document.addEventListener('DOMContentLoaded', function () {

    // --- Chart.js Initialization (Chart.js Başlatma) ---

    // Global Defaults for Dark Mode (Karanlık Mod için Global Varsayılanlar)
    Chart.defaults.color = '#94a3b8'; // Slate-400 for text (Metin için Slate-400)
    Chart.defaults.borderColor = '#334155'; // Slate-700 for grid lines (Izgara çizgileri için Slate-700)

    // Sales Chart (Line Chart) (Satış Grafiği (Çizgi Grafik))
    const salesCtx = document.getElementById('salesChart').getContext('2d');

    // Gradient Fill (Gradyan Dolgu)
    const salesGradient = salesCtx.createLinearGradient(0, 0, 0, 400);
    salesGradient.addColorStop(0, 'rgba(99, 102, 241, 0.5)'); // Indigo 500 (Çivit Mavisi 500)
    salesGradient.addColorStop(1, 'rgba(99, 102, 241, 0.0)');

    const salesChart = new Chart(salesCtx, {
        type: 'line',
        data: {
            labels: ['Jan', 'Feb', 'Mar', 'Apr', 'May', 'Jun', 'Jul', 'Aug', 'Sep', 'Oct', 'Nov', 'Dec'],
            datasets: [{
                label: 'Sales ($)',
                data: [12000, 19000, 3000, 5000, 2000, 3000, 7000, 11000, 15000, 20000, 25000, 30000],
                backgroundColor: salesGradient,
                borderColor: '#6366f1', // Indigo 500 (Çivit Mavisi 500)
                borderWidth: 3,
                tension: 0.4,
                fill: true,
                pointBackgroundColor: '#1e293b', // Slate 800 (Card bg) (Slate 800 (Kart arka planı))
                pointBorderColor: '#6366f1',
                pointBorderWidth: 2,
                pointRadius: 4,
                pointHoverRadius: 6
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    display: false
                },
                tooltip: {
                    backgroundColor: '#1e293b',
                    titleColor: '#fff',
                    bodyColor: '#cbd5e1', // Slate 300 (Slate 300)
                    borderColor: '#334155',
                    borderWidth: 1,
                    padding: 10,
                    displayColors: false,
                }
            },
            scales: {
                y: {
                    beginAtZero: true,
                    grid: {
                        color: '#334155', // Slate 700 (Slate 700)
                        borderDash: [5, 5]
                    },
                    ticks: {
                        callback: function (value) {
                            return '$' + value / 1000 + 'k';
                        }
                    }
                },
                x: {
                    grid: {
                        display: false
                    }
                }
            },
            interaction: {
                intersect: false,
                mode: 'index',
            },
        }
    });

    // Traffic Sources Chart (Doughnut Chart) (Trafik Kaynakları Grafiği (Halka Grafik))
    const trafficCtx = document.getElementById('trafficChart').getContext('2d');
    const trafficChart = new Chart(trafficCtx, {
        type: 'doughnut',
        data: {
            labels: ['Search Engine', 'Direct', 'Social Media', 'Referral'],
            datasets: [{
                data: [45, 25, 20, 10],
                backgroundColor: [
                    '#6366f1', // Indigo 500 (Çivit Mavisi 500)
                    '#10b981', // Emerald 500 (Zümrüt Yeşili 500)
                    '#eab308', // Yellow 500 (Sarı 500)
                    '#ef4444'  // Red 500 (Kırmızı 500)
                ],
                borderColor: '#1e293b', // Match card background (Kart arka planıyla eşleştir)
                borderWidth: 4,
                hoverOffset: 4
            }]
        },
        options: {
            responsive: true,
            maintainAspectRatio: false,
            plugins: {
                legend: {
                    position: 'bottom',
                    labels: {
                        usePointStyle: true,
                        padding: 20,
                        color: '#cbd5e1' // Slate 300 (Slate 300)
                    }
                }
            },
            cutout: '75%'
        }
    });


    // --- Leaflet Map Initialization (Leaflet Harita Başlatma) ---
    // Istanbul coordinates: 41.0082, 28.9784 (İstanbul koordinatları: 41.0082, 28.9784)
    const map = L.map('map').setView([41.0082, 28.9784], 10);

    // Dark Mode Tiles (CartoDB Dark Matter) (Karanlık Mod Karoları (CartoDB Dark Matter))
    L.tileLayer('https://{s}.basemaps.cartocdn.com/dark_all/{z}/{x}/{y}{r}.png', {
        attribution: '&copy; <a href="https://www.openstreetmap.org/copyright">OpenStreetMap</a> contributors &copy; <a href="https://carto.com/attributions">CARTO</a>',
        subdomains: 'abcd',
        maxZoom: 20
    }).addTo(map);

    // Add some random markers to simulate user locations (Kullanıcı konumlarını simüle etmek için rastgele işaretçiler ekleyin)
    const locations = [
        { lat: 41.0082, lng: 28.9784, title: "Istanbul - HQ" },
        { lat: 40.9904, lng: 29.0207, title: "Kadikoy Branch" },
        { lat: 41.0422, lng: 29.0067, title: "Besiktas Hub" },
        { lat: 41.0116, lng: 28.9329, title: "Fatih Store" }
    ];

    locations.forEach(loc => {
        L.marker([loc.lat, loc.lng]).addTo(map)
            .bindPopup(`<div style="color: #333;"><b>${loc.title}</b><br>Active Users: ${Math.floor(Math.random() * 100)}</div>`);
    });

});
