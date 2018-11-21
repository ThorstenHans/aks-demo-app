provider "azurerm" {}

resource "azurerm_resource_group" "demo" {
  name     = "${var.resource_group_name}"
  location = "West Europe"
  tags     = "${var.tags}"
}

resource "azurerm_storage_account" "demo" {
  name                     = "thhsessionexports"
  resource_group_name      = "${azurerm_resource_group.demo.name}"
  location                 = "${azurerm_resource_group.demo.location}"
  account_tier             = "Standard"
  account_replication_type = "LRS"
  tags                     = "${var.tags}"
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

resource "azurerm_container_registry" "demo" {
  name                = "${var.acr_name}"
  resource_group_name = "${azurerm_resource_group.demo.name}"
  location            = "${azurerm_resource_group.demo.location}"
  admin_enabled       = false
  sku                 = "${var.acr_sku}"
  tags                = "${var.tags}"
}

resource "azurerm_storage_account" "azfnstorage" {
  name                     = "thhaksdemostorage"
  resource_group_name      = "${azurerm_resource_group.demo.name}"
  location                 = "${azurerm_resource_group.demo.location}"
  account_tier             = "Standard"
  account_replication_type = "LRS"
}

resource "azurerm_app_service_plan" "azfnappsvcplan" {
  name                = "thh-aks-demo-azfn-app-svc-plan"
  resource_group_name = "${azurerm_resource_group.demo.name}"
  location            = "${azurerm_resource_group.demo.location}"
  kind                = "FunctionApp"

  sku {
    tier = "Dynamic"
    size = "Y1"
  }
}

resource "azurerm_function_app" "azfnapp" {
  name                      = "thh-aks-demo-azfn-app"
  resource_group_name       = "${azurerm_resource_group.demo.name}"
  location                  = "${azurerm_resource_group.demo.location}"
  storage_connection_string = "${azurerm_storage_account.azfnstorage.primary_connection_string}"
  app_service_plan_id       = "${azurerm_app_service_plan.azfnappsvcplan.id}"
  version                   = "beta"

  app_settings {
    APPINSIGHTS_INSTRUMENTATIONKEY = "${var.appinsights_key}"
    SendGridKey                    = "${var.sendgrid_key}"
  }
}

resource "azurerm_kubernetes_cluster" "demo" {
  name                = "${var.aks_name}"
  location            = "${azurerm_resource_group.demo.location}"
  resource_group_name = "${azurerm_resource_group.demo.name}"

  linux_profile {
    admin_username = "azureuser"

    ssh_key {
      key_data = "${file("${var.ssh_public_key}")}"
    }
  }

  dns_prefix = "${var.aks_dns_prefix}"

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
