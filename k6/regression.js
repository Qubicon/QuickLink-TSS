import http from 'k6/http';
import { check } from 'k6';

export const options = {
    thresholds: {
        checks: ['rate==1'],
        // http_req_duration: [{ threshold: 'p(99) < 10', abortOnFail: true }]
    },
};

export default function () {
    const code = testShortenUrl();
    console.log('Generated code:', code);
    testRedirectToOriginalUrl(code);
    testGetAllUrls();
}

function testGetAllUrls() {
    let res = http.get('https://localhost:32768/api/v1/urls', {
        tls_skip_verify: true,
    });

    check(res, {
        'GET /api/v1/urls - status is 200': (r) => r.status === 200,
        'GET /api/v1/urls - retrieved all URLs': (r) => {
            let body = JSON.parse(r.body);
            return Array.isArray(body) && body.length > 0;
        },
    });
}

function testShortenUrl() {
    let code = ""
    let randomURL = `https://drive.google.com/file/d/1vOC8Po8uhVcIRhAofk_D0wGqdKzXUKEG/edit`;
    let shortenUrlReq = { originalUrl: randomURL };
    let shortenUrlRes = http.post('https://localhost:32768/api/v1/url', JSON.stringify(shortenUrlReq), {
        headers: {
            'Content-Type': 'application/json'
        },
        tls_skip_verify: true,
    });

    check(shortenUrlRes, {
        'POST /api/v1/url - status is 200': (r) => r.status === 200,
        'POST /api/v1/url - shortened URL is returned': (r) => {
            let responseJson = JSON.parse(r.body);
            code = responseJson.shortenedUrl.split('/').pop();
            return responseJson.shortenedUrl !== undefined;
        },
    });

    return code;
}

function testRedirectToOriginalUrl(code) {
    let shortenedUrl = `https://localhost:32768/api/v1/${code}`;
    let redirectUrlRes = http.get(shortenedUrl, {
        tls_skip_verify: true,
    });

    check(redirectUrlRes, {
        'GET /api/v1/{code} - status is 200': (r) => r.status === 200,
    });
}