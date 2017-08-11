<template>
  <div>
    <mu-tabs :value="activeTab" @change="handleTabChange">
      <mu-tab value="tab1" href="login" title="Login"/>
      <mu-tab value="tab2" title="Search"/>
      <mu-tab value="tab3" @active="handleActive" title="TAB ACTIVE"/>
    </mu-tabs>
  </div>
</template>
<script>
  export default{
    name: 'png-nav'
  }
</script>

