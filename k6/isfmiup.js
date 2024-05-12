import http from 'k6/http';
import { check } from 'k6';

export default function () {
    let response = http.get("https://fmi.unibuc.ro/");

    check(response, {
        'status is 200': (r) => r.status === 200
    });
}