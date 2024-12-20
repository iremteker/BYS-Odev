document.addEventListener('DOMContentLoaded', function () {
    const studentTab = document.querySelector('.tab.student');
    const advisorTab = document.querySelector('.tab.advisor');
    const loginForm = document.querySelector('.login-form');

    // Tab seçimini kontrol etme
    studentTab.addEventListener('click', function () {
        setActiveTab('student');
    });

    personnelTab.addEventListener('click', function () {
        setActiveTab('advisor');
    });

    // Sekmeyi aktif etme
    function setActiveTab(type) {
        if (type === 'student') {
            studentTab.classList.add('active');
            advisorTab.classList.remove('active');
            loginForm.action = '/Student/StudentDashboard'; // Öğrenci form action
        } else if (type === 'advisor') {
            advisorTab.classList.add('active');
            studentTab.classList.remove('active');
            loginForm.action = '/Advisor/AdvisorDashboard'; // Advisor form action
        }
    }

    // Sayfa yüklenirken aktif sekmeyi ayarlama

    if (studentTab.classList.contains('active')) {
        setActiveTab('student');
    } else if (advisorTab.classList.contains('active')) {
        setActiveTab('advisor');
    }
});