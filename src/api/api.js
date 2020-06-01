export function httpGet(path) {
  return req(path)
}

export function httpPost(path, data) {
  return req(path, 'POST', data)
}

export function httpPut(path, data) {
  return req(path, 'PUT', data)
}

const apiUrl = 'https://localhost:5001'

async function req(path, method = 'GET', data) {
  const res = await fetch(apiUrl + path, {
    method,
    headers: {
      'Origin': 'http://localhost:5000'
    },
    body: data && JSON.stringify(data)
  })
  const json = await res.json()
  return { ok: res.ok, data: json }
}
