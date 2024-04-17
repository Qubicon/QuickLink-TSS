import http from 'k6/http';
import { check } from 'k6';
import test from './test';

export default function () {
  let res = http.get('https://localhost:32768/api/v1/urls', {
    tls_skip_verify: true,
  });

  check(res, {
    'status is 200': (r) => r.status === 200,
    'retrieved all URLs': (r) => {
      let body = JSON.parse(r.body);
      return Array.isArray(body) && body.length > 0;
    },
  });
}
