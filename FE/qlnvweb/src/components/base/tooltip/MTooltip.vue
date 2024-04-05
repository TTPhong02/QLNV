<template>
  <div>
    <div @mouseover="showTooltip" @mouseout="hideTooltip">
      <slot></slot>
      <div v-if="isTooltipVisible" class="tooltip">
        {{ content }}
      </div>
    </div>
  </div>
</template>

<script>
export default {
    name:"MTooltip",
  data() {
    return {
      isTooltipVisible: false,
    };
  },
  props: {
    content: {
      type: String,
      default: "ABCDEF",
    },
  },
  methods: {
    showTooltip() {
      this.isTooltipVisible = true;
    },
    hideTooltip() {
      this.isTooltipVisible = false;
    },
  },
};
</script>

<style>
.tooltip {
    position: relative;
    display: inline-block;
  }
  
  .tooltip .tooltiptext {
    visibility: hidden;
    width: 100px;
    background-color: rgba(224, 221, 221,0.8);
    color: #000000;
    text-align: center;
    border-radius: 6px;
    padding: 5px 0;
    top: 50%;
    right:100% ;
    /* Position the tooltip */
    position: absolute;
    z-index: 10000;
  }
  
  .tooltip:hover .tooltiptext {
    visibility: visible;
  }
</style>