let ipInput = document.getElementById("ip");
let vlanInput = document.getElementById("vlan");
let macInput = document.getElementById("mac");
let serialNumberInput = document.getElementById("serialNumber");
let stockNumberInput = document.getElementById("stockNumber");
let ipMask = {
    mask: 'IP{.}`IP{.}`IP{.}`IP',
    blocks: {
        IP: {
            mask: [
                {
                    mask: "\\0"
                },
                {
                    mask: Number,
                    scale: 0,
                    min: 0,
                    max: 255
                }
            ]
        }
    }
}
let macMask = {
    mask: 'MAC{:}`MAC{:}`MAC{:}`MAC{:}`MAC{:}`MAC',
    blocks: {
        MAC: {
            mask: /^[0-9A-Fa-f]{0,2}$/
        }
    }
}
let serialNumberMask = {
    mask: /^(\d|\w)*$/
}
let stockNumberMask = {
    mask: /^(\d)*$/
}
let ip = IMask(ipInput, ipMask);
let mac = IMask(macInput, macMask);
let serialNumber = IMask(serialNumberInput, serialNumberMask);
let stockNumber = IMask(stockNumberInput, stockNumberMask);