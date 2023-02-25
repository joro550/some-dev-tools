print("""
-----------------------------------------------------------------
✨ Hello Tilt! This appears in the (Tiltfile) pane whenever Tilt
   evaluates this file.
-----------------------------------------------------------------
""".strip())

local_resource(
    'publish_web',
    'dotnet publish ./Server/DevTools.Server.csproj -o out/devtools',
    ignore= ['./Server/bin', './Server/obj', './Client/bin', './Client/obj'],
    deps= ['./Server', './Client'],
    allow_parallel=True,
)

local_resource(
    'firebase_emulator',
    serve_cmd= 'firebase emulators:start',
    allow_parallel=True
)

docker_build(
    'dev_tools',
    '/out/devtools',
    dockerfile='tilt/tilt.Dockerfile',
    live_update=[
        sync('/out/devtools', '/App'),
        restart_container()
    ]
)

local_resource(
    'build_css',
    'npx tailwindcss -c ./tailwind.config.js -i ./Client/wwwroot/css/input.css -o ./out/devtools/wwwroot/css/output.css',
    deps= ['./Client'],
    ignore= ['./Client/bin', './Client/obj', './Client/wwwroot/css/output.css'],
    allow_parallel=True,
)

docker_compose('./tilt/docker-compose.yml')
