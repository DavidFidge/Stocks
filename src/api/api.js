export function httpGet(path) {
  return getRequest(path)
}

export function httpPost(path, data) {
  return postPutRequest(path, 'POST', data)
}

export function httpPut(path, data) {
  return postPutRequest(path, 'PUT', data)
}

const apiUrl = 'https://localhost:5001'

async function getRequest(path) {
   
  const res = await fetch(apiUrl + path, {
    method: 'GET',
    headers: {
      'Origin': 'http://localhost:5000',
      'Content-Type': 'application/json'
    }
  })
  const json = await res.json()
  return { ok: res.ok, data: json }
}

async function postPutRequest(path, method, data) {
    const res = await fetch(apiUrl + path, {
      method,
      headers: {
        'Origin': 'http://localhost:5000',
        'Content-Type': 'application/json'
      },
      body: data && JSON.stringify(data)
    })

    return { ok: res.ok }
  }
  
