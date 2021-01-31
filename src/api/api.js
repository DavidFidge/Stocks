import { apiUrl, apiOrigin } from './apiUrl.js'

export function httpGet(path) {
  return getRequest(path)
}

export function httpPost(path, data) {
  return postPutRequest(path, 'POST', data)
}

export function httpPut(path, data) {
  return postPutRequest(path, 'PUT', data)
}

export function getApiUrl() {
  return apiUrl();
}

async function getRequest(path) {
   
  const res = await fetch(apiUrl + path, {
    method: 'GET',
    headers: {
      'Origin': apiOrigin(),
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
        'Origin': apiOrigin(),
        'Content-Type': 'application/json'
      },
      body: data && JSON.stringify(data)
    })

    let tryGetJson = async (resp) => {
      return new Promise((resolve) => {
        if (resp) {
          resp.json().then(json => resolve(json)).catch(() => resolve(null))
        } else {
          resolve(null)
        }
      })
    }

    const json = await tryGetJson(res);

    return { ok: res.ok, response: json }
  }
  
