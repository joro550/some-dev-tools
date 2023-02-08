# Welcome to Tilt!
#   To get you started as quickly as possible, we have created a
#   starter Tiltfile for you.
#
#   Uncomment, modify, and delete any commands as needed for your
#   project's configuration.


# Output diagnostic messages
#   You can print log messages, warnings, and fatal errors, which will
#   appear in the (Tiltfile) resource in the web UI. Tiltfiles support
#   multiline strings and common string operations such as formatting.
#
#   More info: https://docs.tilt.dev/api.html#api.warn
print("""
-----------------------------------------------------------------
✨ Hello Tilt! This appears in the (Tiltfile) pane whenever Tilt
   evaluates this file.
-----------------------------------------------------------------
""".strip())
warn('ℹ️ Open {tiltfile_path} in your favorite editor to get started.'.format(
    tiltfile_path=config.main_path))

local_resource(
    'build_web',
    'dotnet build ./Server/DevTools.Server.csproj',
    serve_cmd= 'dotnet run --project ./Server/DevTools.Server.csproj --no-build',
    ignore= ['./Server/bin', './Server/obj', './Client/bin', './Client/obj'],
    deps= ['./Server', './Client'],
    allow_parallel=True,
)

local_resource(
    'build_css',
    'npx tailwindcss -c ./tailwind.config.js -i ./Client/wwwroot/css/input.css -o ./Client/wwwroot/css/output.css',
    deps= ['./Client'],
    ignore= ['./Client/bin', './Client/obj', './Client/wwwroot/css/output.css'],
    allow_parallel=True,
)

