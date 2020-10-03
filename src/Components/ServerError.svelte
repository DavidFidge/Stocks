<script>
    export let action = "";
    export let errorDescription = "";
</script>

<style>
</style>

{#if errorDescription.length > 0}
<div>
    An error occurred when performing {action}: {errorDescription}
</div>
{/if}
