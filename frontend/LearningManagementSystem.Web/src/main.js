const response = await fetch(
    "http://localhost:5031/api/course-offerings"
);

const courseOfferings = await response.json();

console.log(courseOfferings);