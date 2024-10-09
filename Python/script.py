import win32pipe, win32file, pywintypes
import time

pipe = r'\\.\pipe\MainPipe'

while True:
    try:
        print("Connecting...")
        handle = win32file.CreateFile(
            pipe,
            win32file.GENERIC_READ | win32file.GENERIC_WRITE,
            0,
            None,
            win32file.OPEN_EXISTING,
            0,
            None
        )

        print("Connection Sucessful")
        break

    except pywintypes.error as e:
        print(f"error : {e}")
        print("Waiting...")
        time.sleep(1)

print("sending msg")
message = "asd"
win32file.WriteFile(handle, message.strip().encode())

try:
    print("Waiting for response")
    result,data = win32file.ReadFile(handle, 64 * 1024)
    print(f"recieved {data.encode('utf-8').strip()}")
except Exception as e:
    print(f"error {e}")

finally:
    win32file.CloseHandle(handle)
    print("closing handle")