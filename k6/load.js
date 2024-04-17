import http from 'k6/http';
import { check, sleep } from 'k6';

export let options = {
    stages: [
        // Ramp-up from 1 to 50 VUs (Virtual Users) in 10 seconds
        { duration: '10s', target: 50 },
        // Maintain 50 VUs for 1 minute
        { duration: '1m', target: 50 },
        // Ramp-down from 50 to 0 VUs in 10 seconds
        { duration: '10s', target: 0 },
    ],
    thresholds: {
        // Ensure that the error rate is less than 1%
        http_req_failed: ['rate<0.01'],
        // Ensure that the average response time is below 2000 ms
        http_req_duration: ['p(95)<=400'],
    },
};

export default function () {
    let randomURL = `https://example.com/${Math.random().toString(36).substring(7)}`;
    let urlCreationRequest = { originalUrl: randomURL };

    let shortenRes = http.post('https://localhost:32768/api/v1/url', JSON.stringify(urlCreationRequest), {
        headers: {
            'Content-Type': 'application/json'
        },
        tls_skip_verify: true,
    });

    check(shortenRes, {
        'status is 200': (r) => r.status === 200,
    });

    // Sleep for a short duration before making the next request
    sleep(0.2);
}
