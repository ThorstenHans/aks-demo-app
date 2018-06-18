provider "azurerm" {}

resource "azurerm_resource_group" "demo" {
  name     = "${var.resource_group_name}"
  location = "West Europe"
  tags     = "${var.tags}"
}

resource "azurerm_resource_group" "demoaks" {
  name     = "${var.aks_resource_group_name}"
  location = "West Europe"
  tags     = "${var.tags}"
}

resource "azurerm_sql_server" "demo" {
  name                         = "thh-demo-sql-server"
  resource_group_name          = "${azurerm_resource_group.demo.name}"
  location                     = "${azurerm_resource_group.demo.location}"
  version                      = "12.0"
  administrator_login          = "testadmin"
  administrator_login_password = "thisIsSuperSecur3!"
  tags                         = "${var.tags}"
}

resource "azurerm_sql_database" "demo" {
  name                = "sessions"
  resource_group_name = "${azurerm_resource_group.demo.name}"
  location            = "${azurerm_resource_group.demo.location}"
  server_name         = "${azurerm_sql_server.demo.name}"
  tags                = "${var.tags}"
}

resource "azurerm_container_registry" "test" {
  name                = "${var.acr_name}"
  resource_group_name = "${azurerm_resource_group.demo.name}"
  location            = "${azurerm_resource_group.demo.location}"
  admin_enabled       = false
  sku                 = "${var.acr_sku}"
  tags                = "${var.tags}"
}

resource "azurerm_kubernetes_cluster" "demo" {
  name                = "${var.aks_name}"
  location            = "${azurerm_resource_group.demo.location}"
  resource_group_name = "${azurerm_resource_group.demoaks.name}"
  kubernetes_version  = "${var.k8s_version}"

  linux_profile {
    admin_username = "azureuser"

    ssh_key {
      key_data = "${file("${var.ssh_public_key}")}"
    }
  }

  dns_prefix = "thhtestprefix"

  agent_pool_profile {
    name    = "default"
    count   = "${var.aks_agent_vm_count}"
    vm_size = "${var.aks_agent_vm_size}"
    os_type = "Linux"
  }

  service_principal {
    client_id     = "${var.service_principal_id}"
    client_secret = "${var.service_principal_password}"
  }

  tags = "${var.tags}"
}
