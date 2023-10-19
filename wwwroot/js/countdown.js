// // Initialize with a date-time string from the server (or hardcode as shown below)

// const timer = (expirationDate) => {
//     var currentDate = new Date();
//     var remainingTime = expirationDate - currentDate;

//     // Check if timer should stop
//     if (remainingTime < 0) {
//         clearInterval(interval);
//         document.getElementById("countdown").innerHTML = "EXPIRED";
//         return;
//     }

//     // Calculate remaining time components
//     var days = Math.floor(remainingTime / (1000 * 60 * 60 * 24));
//     var hours = Math.floor((remainingTime % (1000 * 60 * 60 * 24)) / (1000 * 60 * 60));
//     var minutes = Math.floor((remainingTime % (1000 * 60 * 60)) / (1000 * 60));
//     var seconds = Math.floor((remainingTime % (1000 * 60)) / 1000);

//     // Update HTML elements
//     document.getElementById("days").innerHTML = days;
//     document.getElementById("hours").innerHTML = hours;
//     document.getElementById("minutes").innerHTML = minutes;
//     document.getElementById("seconds").innerHTML = seconds;
// }


// // Update the timer every 1 second
// var interval = setInterval(timer, 1000);
